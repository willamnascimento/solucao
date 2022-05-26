import { CalendarItemComponent } from './components/calendar-item/calendar-item.component';
import { StickyNotesService } from './../../shared/services/sticky-notes.service';
import { StickyNotesTableComponent } from './components/sticky-notes-table/sticky-notes-table.component';
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
import { CalendarRoutingModule } from './calendar-routing.module';
import { CalendarPageComponent } from './container/calendar-page/calendar-page.component';
import { CalendarTableComponent } from './components/calendar-table/calendar-table.component';
import { CalendarDialogComponent } from './components/calendar-dialog/calendar-dialog.component';
import { CalendarService } from 'src/app/shared/services/calendar.service';
import { TextMaskModule } from 'angular2-text-mask';

@NgModule({
  declarations: [
      CalendarPageComponent,
      CalendarTableComponent,
      CalendarDialogComponent,
      StickyNotesTableComponent,
      CalendarItemComponent
  ],
  imports: [
    CommonModule,
    CalendarRoutingModule,
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
    CalendarService,
    StickyNotesService
  ]
})
export class CalendarModule { }
