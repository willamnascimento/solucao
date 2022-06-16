import { StickyNotes } from './../../../../shared/models/stickyNotes';
import { StickyNotesService } from './../../../../shared/services/sticky-notes.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MatTableDataSource } from '@angular/material/table';
import { Calendar } from 'src/app/shared/models/calendar';
import { MY_FORMATS } from 'src/app/consts/my-format';
import moment from 'moment';



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
      let time = moment(this.today, "DD/MM/YYYY");
      this.stickyNotesService.loadStickyNotes(time.format('YYYY-MM-DD')).subscribe((resp: StickyNotes[]) => {
        this.dataSource = new MatTableDataSource<StickyNotes>();
        this.dataSource = new MatTableDataSource<StickyNotes>(resp);
      });
    }
  }