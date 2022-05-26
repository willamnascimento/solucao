import { StickyNotes } from './../../../../shared/models/stickyNotes';
import { StickyNotesService } from './../../../../shared/services/sticky-notes.service';
import { ToastrService } from 'ngx-toastr';
import { SpecificationsService } from 'src/app/shared/services/specifications.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, ElementRef, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MatTableDataSource } from '@angular/material/table';
import { Calendar } from 'src/app/shared/models/calendar';
import { CalendarService } from 'src/app/shared/services/calendar.service';
import { CalendarDialogComponent } from '../calendar-dialog/calendar-dialog.component';
import moment from 'moment';
import { Specification } from 'src/app/shared/models/specification';
import { noop } from 'rxjs';
import { MY_FORMATS } from 'src/app/consts/my-format';



@Component({
    selector: 'app-sticky-notes-table',
    templateUrl: 'sticky-notes-table.component.html',
    styleUrls: ['./sticky-notes-table.component.scss',],
    providers: [
      {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
      {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
    ],
  })
  export class StickyNotesTableComponent implements OnChanges{
    
    displayedColumns: string[] = ['anotacao', 'resolvido','acoes'];
    dataSource: MatTableDataSource<StickyNotes> = new MatTableDataSource<StickyNotes>();
    selection = new SelectionModel<Calendar>(true, []);
    selectedTabIndex = 0;
    isShowFilterInput = false;
    innerValue: Date = new Date();
    @Input() today: string;

    constructor(private stickyNotesService: StickyNotesService) {
      
    }

    ngOnChanges(changes: SimpleChanges): void {
      this.getStickyNotes();
    }

    getStickyNotes(){
      console.log(this.today);
      this.stickyNotesService.loadStickyNotes(this.today).subscribe((resp: StickyNotes[]) => {
          this.dataSource = new MatTableDataSource<StickyNotes>();
          this.dataSource = new MatTableDataSource<StickyNotes>(resp);
      });
    }
  }