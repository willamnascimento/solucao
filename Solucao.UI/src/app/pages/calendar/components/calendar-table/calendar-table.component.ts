import { filter } from 'rxjs/operators';
import { Equipament } from './../../../../shared/models/equipament';
import { EquipamentsService } from './../../../../shared/services/equipaments.service';
import { ToastrService } from 'ngx-toastr';
import { SpecificationsService } from 'src/app/shared/services/specifications.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
    selector: 'app-calendar-table',
    templateUrl: 'calendar-table.component.html',
    styleUrls: ['./calendar-table.component.scss',],
    providers: [
      {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
      {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
    ],
  })
  export class CalendarTableComponent implements OnInit{
    
    displayedColumns: string[] = ['equipamento', 'locatario', 'horario', 'tecnica', 'motorista','usuario','status','obs'];
    @ViewChild('inputSearch') inputSearch: ElementRef;
    dataSource: Calendar[];
    isShowFilterInput = false;
    currentDate = new Date();
    specificationArray: Specification[];
    equipamentArray: Equipament[];
    time;
    value;
    todayDate;
    inputReadonly = false;
    innerValue: Date = new Date();

    constructor(private calendarService: CalendarService,
                public dialog: MatDialog,
                private specificationSerivce: SpecificationsService,
                private equipamentService: EquipamentsService,
                private toastrService: ToastrService) {
      this.time = moment();
    }

    showFilterInput(): void {
      this.isShowFilterInput = !this.isShowFilterInput;
    }

    closeFilterInput(): void {
      this.time = moment(new Date(), 'DD/MM/YYYY', true);
      this.inputSearch.nativeElement.value = '';
      this.getCalendars();
    }

    applyFilter(event): void {
      let length = this.inputSearch.nativeElement.value.length;
      let charCode = (event.which) ? event.which : event.keyCode;
      if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) || length < 10) {
        event.preventDefault();
        return;
      }
      
      let data = moment(this.inputSearch.nativeElement.value, 'DD-MM-YYYY', true).isValid()
      if (!data){
        this.toastrService.info("Data está incorreta!");
        return;
      }

      this.time = moment(this.inputSearch.nativeElement.value, 'DD-MM-YYYY', true);
      this.getCalendars();
    }

    ngOnInit(): void {
      this.getCalendars();
      this.getEquipament();
      this.loadSpecifications();
    }

    async loadSpecifications(): Promise<void> {
      await this.specificationSerivce.loadSpecifications().toPromise().then((data) => {
        localStorage.setItem('specificationsList',JSON.stringify(data));
        this.specificationArray = data;
      }); 
    }

    getCalendars(): void{
      let date = this.time.format('YYYY-MM-DD');
      this.calendarService.getCalendarByDay(date).subscribe((resp: Calendar[]) => {
        this.dataSource = resp;
      });
    }

    getEquipament(): void {
      this.equipamentService.loadEquipaments(true).subscribe((resp: Equipament[]) => {
        this.equipamentArray = resp;
      });
      
    }

    filterItemsByEquipament(item: Equipament): Calendar[] {
      return this.dataSource.filter(x => x.equipamentId === item.id);
    }

    openDialog(element: Calendar){
      const dialogRef = this.dialog.open(CalendarDialogComponent, {
        width: '700px',
        height: '600px',
        disableClose: true,
        data: {element}
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result === undefined)
          return;
        
        this.getCalendars();           
      });
    }

    descriptionSpecifications(item: Calendar){
      let retorno = new Array();
      item.calendarSpecifications.forEach(obj => {
        if (obj.active === true){
          let name = this.specificationArray?.find(x => x.id === obj.specificationId);
          if (name){
            retorno.push(name.name);
          }
        }   
      });
      return retorno.join(' - ')
    }

    showTime(item: Calendar){
      let start = ''
      let end = '';
      if (item.startTime)
        start = item.startTime.substring(11,16);
      if (item.endTime)
        end = item.endTime.substring(11,16)
      return start + ' - ' + end;
    }

    statusToString(status){
      let ret = 'Confirmada';

      if (status === '2'){
        ret = 'Pendente';
      }else if (status === '3'){
        ret = 'Cancelada';
      }else if (status === '4'){
        ret = 'Excluida';
      }

      return ret;
    }
  }