import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { LayoutService } from '../../services/layout/layout-service';
import { Sidebar } from "../../components/sidebar/sidebar";
import { Header } from "../../components/header/header";

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, Sidebar, Header],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
  providers: [AsyncPipe,DecimalPipe]

})
export class MainLayout {
  layoutS=inject(LayoutService);

}
