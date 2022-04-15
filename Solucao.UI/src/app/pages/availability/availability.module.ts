import { AvailabilityTableComponent } from './components/availability-table/availability-table.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from '../../shared/shared.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { MatTabsModule } from '@angular/material/tabs';
import { CalendarService } from 'src/app/shared/services/calendar.service';
import { TextMaskModule } from 'angular2-text-mask';
import { AvailabilityPageComponent } from './container/availability-page.component';
import { AvailabilityRoutingModule } from './availability-routing.module';

@NgModule({
  declarations: [
      AvailabilityPageComponent,
      AvailabilityTableComponent
  ],
  imports: [
    CommonModule,
    AvailabilityRoutingModule,
    MatCardModule,
    MatIconModule,
    MatMenuModule,
    MatTableModule,
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatDialogModule,
    MatTabsModule,
    SharedModule,
    TextMaskModule,
    NgxMaskModule.forChild(),
  ],
  providers: [
    CalendarService
  ]
})
export class AvailabilityModule { }
