import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { LayoutService } from '../../services/layout/layout-service';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
  providers: [AsyncPipe,DecimalPipe]

})
export class MainLayout {
  layoutS=inject(LayoutService);

}
