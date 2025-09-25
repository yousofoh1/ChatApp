/* eslint-disable @typescript-eslint/adjacent-overload-signatures */
import { ElementRef, inject, Injectable, PipeTransform, signal } from '@angular/core';

import { BehaviorSubject, Observable, of, Subject } from 'rxjs';

import { DecimalPipe } from '@angular/common';
import { catchError, debounceTime, delay, distinctUntilChanged, finalize, skip, switchMap, tap } from 'rxjs/operators';
import { HttpClient, HttpContext, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { IResponse } from './base-models';
// import { DeleteService } from './delete/delete-service';
// import { ToastrService } from 'ngx-toastr';
import { NavigationEnd, Router } from '@angular/router';
interface IItem {
  [key: string]: any;
}

interface IPostOptions {
  headers?: HttpHeaders | Record<string, string | string[]>;
  context?: HttpContext;
  observe?: 'body';
  params?: HttpParams | Record<string, string | number | boolean | ReadonlyArray<string | number | boolean>>;
  reportProgress?: boolean;
  responseType?: 'json';
  withCredentials?: boolean;
  keepalive?: boolean;
  transferCache?:
    | {
        includeHeaders?: string[];
      }
    | boolean;
}

export interface BaseServiceState {
  pageIndex: number;
  pageSize: number;
  searchTerm: string;
  searchColumn: number;
  totalPagesCount: number;
  totalRowsCount: number;
}

export default class BaseService<T extends IItem> {
  route = '';
  searchResult = signal<any | null>(null);
  get url() {
    return `${environment.rootUrl}/api/${this.route}/`;
  }
  items = signal<T[]>([]);
  searchedItems = signal<T[]>([]);
  initialValue = { pageIndex: 1, pageSize: 10 };
  search$ = new BehaviorSubject<any>(this.initialValue);

  _state: BaseServiceState = {
    pageIndex: 1,
    pageSize: 10,
    searchTerm: '',
    searchColumn: 0,
    totalPagesCount: 0,
    totalRowsCount: 0,
  };

  _routes = {
    add: 'manage',
    get: 'listAsync',
    getById: 'getById',
    search: 'search',
    delete: 'delete',
  };
  get routes() {
    return this._routes;
  }
  patchRoute(obj: { [key: string]: string }) {
    Object.assign(this.routes, obj);
  }

  httpClient = inject(HttpClient);
  // deleteService = inject(DeleteService);
  // toastService = inject(ToastrService);
  constructor(route: string, refreshRoutes: string[] = [], private router: Router = inject(Router)) {
    this.route = route;
    this.start();
    this.router.events.subscribe((event) => {
      if (this.searchTerm) {
        this.searchTerm = '';
      }
      if (event instanceof NavigationEnd) {
        refreshRoutes.forEach((e) => {
          let url = event.urlAfterRedirects;

          if (url.split('/').length > 2) {
            let parts = url.split('/');
            url = parts[0] + '/' + parts[2];
          }

          if (url.slice(1) === e) {
            if (this._state.pageIndex > 1) {
              this.getPage(1).subscribe();
            }
            return; // Stop after the first match
          }
        });
      }
    });
  }

  getPaginationDto(): any {
    return {
      pageIndex: this._state.pageIndex,
      pageSize: this._state.pageSize,
    };
  }
  patchState(patch: Partial<BaseServiceState>) {
    Object.assign(this._state, patch);
  }

  start(partialState?: Partial<BaseServiceState>) {
    Object.assign(this._state, partialState || {});
    this.initialValue = {
      pageIndex: this._state.pageIndex,
      pageSize: this._state.pageSize,
    };
    // this.startSearch();
    this.getPage(1).subscribe();
  }
  startSearch() {
    this.search$
      .pipe(
        debounceTime(300),
        // skip(1),
        switchMap((dto) => {
          if (typeof dto == 'string' && dto != '') {
            return this._search().pipe(
              catchError((error) => {
                this.searchedItems.set([]); // handle gracefully
                return of({ paginationInfo: { totalRowsCount: 0, totalPagesCount: 0 }, rows: [] } as IResponse<T>); // allow stream to continue
              })
            );
          } else {
            return this._getPage();
          }
        })
      )
      .subscribe({
        next: (result) => {
          this.searchResult.set(null);
          if ('paginationInfo' in result && result.paginationInfo) {
            // this._state.totalPagesCount = result.paginationInfo.totalPagesCount;
            // this._state.totalRowsCount = result.paginationInfo.totalRowsCount;
          }
          // if (result?.rows?.length == 0) {
          //   // this.toastService.error('لا يوجد نتائج');
          //   this.searchedItems.set([]);
          // } else if (result?.rows?.length == 1) {
          //   this.searchResult.set(result.rows[0]);
          //   this.searchedItems.set(result?.rows ?? []);
          //   console.log(result.rows[0]);
          // } else {
          //   this.searchedItems.set(result?.rows ?? []);
          // }
        },
        error: (error) => {},
      });
  }
  getById(id: any) {
    return this.httpClient.get<any>(this.url + this.routes.getById + '/' + id);
  }
  update(data: any, options: IPostOptions = {} as IPostOptions) {
    return this.httpClient.post<IResponse<T>>(this.url + 'Manage', data, options).pipe(
      tap({
        next: () => {
          this.getPage(1).subscribe();
          // this.search('');
        },
      })
    );
  }
  add<AddType>(data: any) {
    return this.httpClient.post<any>(this.url + this.routes.add, data).pipe(
      tap({
        next: () => {
          this.getPage(1).subscribe();
          // this.search('');
        },
      })
    );
  }
  // delete = (id: number, callBack?: () => void) => {
  //   this.deleteService.done.set(false);
  //   this.deleteService.open({
  //     approve: () =>
  //       this.httpClient
  //         .post<IResponse<T>>(this.url + 'Delete/' + id, null)
  //         .pipe(
  //           tap({
  //             next: () => {
  //               this.getPage(1).subscribe();
  //             },
  //           })
  //         )
  //         .subscribe({
  //           next: (res) => {
  //             this.toastService.success('تم الحذف بنجاح');
  //             this.deleteService.done.set(true);
  //             document.querySelector('form')?.reset();
  //             callBack?.();
  //           },
  //         }),
  //   });
  // };
  private _getPage() {
    return this.httpClient
      .post<IResponse<T>>(this.url + this.routes.get, this.getPaginationDto(), {
        headers: {
          'skip-error': 'true',
        },
      })
      .pipe(
        tap((res) => {
          // Object.assign(this._state, res.paginationInfo);
        })
      );
  }
  getPage(id: number = this._state.pageIndex, size: number = this._state.pageSize) {
    this._state.pageIndex = id;
    this._state.pageSize = size;
    if (this._state.searchTerm && this._state.searchTerm != '') {
      return this._search().pipe(
        tap((res) => {
          // this.searchedItems.set(res.rows);
          // Object.assign(this._state, res.paginationInfo);
        })
      );
    } else {
      return this._getPage().pipe(
        tap((res) => {
          // this.items.set(res.rows);
          // this.searchedItems.set(res.rows);
          // Object.assign(this._state, res.paginationInfo);
        })
      );
    }
  }

  get pageSize() {
    return this._state.pageSize;
  }
  get searchTerm() {
    return this._state.searchTerm;
  }

  set pageSize(pageSize: number) {
    this.patchState({ pageSize });
  }
  set searchTerm(searchTerm: string) {
    this.patchState({ searchTerm });
    if (searchTerm == '') {
      this.getPage().subscribe();
    } else {
      this.search();
    }
  }

  set searchColumn(searchColumn: number) {
    this.patchState({ searchColumn });
    if (this._state.searchTerm == '') {
      this.getPage().subscribe();
    } else {
      this.search();
    }
  }

  private _search(): Observable<IResponse<T>> {
    const { pageIndex, searchTerm, searchColumn } = this._state;

    const dto = {
      criteriaDto: {
        paginationInfo: {
          pageIndex: pageIndex,
          pageSize: 10,
        },
      },
      searchFilterDto: {
        column: searchColumn,
        value: searchTerm,
      },
    };

    return this.httpClient.post<IResponse<T>>(this.url + this._routes.search, dto, {
      headers: {
        'skip-error': 'true',
      },
    });
  }
  search(term?: string) {
    if (term == undefined || term == null) {
      // If term is not provided, use the current search term from the state
      term = this._state.searchTerm;
    }
    term = term.trim();

    if (this._state.searchTerm != term) {
      this._state.pageIndex = 1;
    }
    this.patchState({ searchTerm: term, pageIndex: this._state.pageIndex, pageSize: 10 });
    this.search$.next(term);
    return this.search$;
  }

  getNextPage() {
    let canFetchMore = this._state.pageIndex < this._state.totalPagesCount;
    if (!canFetchMore) {
      return;
    }
    this.patchState({ pageIndex: this._state.pageIndex + 1 });
    if (this._state.searchTerm) {
      this.search$.next(this._state.searchTerm);
    } else {
      this.search('');
    }
  }
}
