import { inject } from '@angular/core';
// import { TranslateService } from '@ngx-translate/core';
// import { ToastrService } from 'ngx-toastr';
// import { PageModesEnum } from '../enums';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
// import { LayoutService } from '../services/layout/layout-service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthS } from '../features/auth/services/auth-s';
import { LayoutS } from '../layouts/layout-s';
import { SignalRS } from '../services/signalr/signal-r-s';
// import { MainSettings } from '../services/main-settings/main-settings';

export abstract class BaseComp {
  //
  // translateService = inject(TranslateService);
  // settingsS=inject(MainSettings);
  // toaster = inject(ToastrService);
  // PageModesEnum = PageModesEnum;
  layoutS = inject(LayoutS);
  initialForm: any;
  fb = inject(FormBuilder);
  router = inject(Router);
  route = inject(ActivatedRoute);
  authS = inject(AuthS);
  signalRS = inject(SignalRS);
  //

  fg!: FormGroup;
  currentDateForInput = new Date().toISOString().split('T')[0];
  /**
   *
   */
  constructor() {}
  InitializeForm() {
    this.fg = this.fb.group(this.initialForm);
  }

  // localize(ar: any, en: any) {
  //   return this.translateService.getCurrentLang() == 'ar' ? ar : en;
  // }

  // translate(key: string) {
  //   return this.translateService.instant(key);
  // }

  truncate(str: string, num: number) {
    return str?.length > num ? str.slice(0, num - 1) + '...' : str;
  }

  // toastSuccess(tMsg: string = 'success') {
  //   let message = this.translateService.instant(`messages.${tMsg}`);
  //   this.toaster.success(message);
  // }
  // toastInfo(tMsg: string) {
  //   let message = this.translateService.instant(`messages.${tMsg}`);
  //   this.toaster.info(message);
  // }
  // toastError(tMsg: string = 'error') {
  //   let message = this.translateService.instant(`messages.${tMsg}`);
  //   this.toaster.error(message);
  // }

  getFC(name: string, fg: FormGroup = this.fg) {
    return fg?.get(name) as FormControl;
  }

  getFCErrors(name: string, fg: FormGroup = this.fg) {
    return fg?.get(name)?.errors;
  }

  generateRandomColor() {
    return '#' + Math.floor(Math.random() * 16777215).toString(16);
  }
}
