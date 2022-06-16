import { filter } from 'rxjs/operators';
import { Equipament } from './../../../../shared/models/equipament';
import { EquipamentsService } from './../../../../shared/services/equipaments.service';
import { ToastrService } from 'ngx-toastr';
import { SpecificationsService } from 'src/app/shared/services/specifications.service';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Calendar } from 'src/app/shared/models/calendar';
import { CalendarService } from 'src/app/shared/services/calendar.service';
import { CalendarDialogComponent } from '../calendar-dialog/calendar-dialog.component';
import moment from 'moment';
import { Specification } from 'src/app/shared/models/specification';
import { MY_FORMATS } from 'src/app/consts/my-format';
import { PersonDialogUpdateComponent } from '../person-dialog-update/person-dialog-update.component';



@Component({
    selector: 'app-calendar-table',
    templateUrl: 'calendar-table.component.html',
    styleUrls: ['./calendar-table.component.scss',],
    providers: [
      {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
      {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
    ],
  })
  export class CalendarTableComponent implements OnInit, AfterViewInit{
    
    displayedColumns: string[] = ['equipamento', 'locatario', 'horario', 'tecnica', 'motorista','usuario','status','obs'];
    @ViewChild('inputSearch') inputSearch: ElementRef;
    dataSource: [];
    isShowFilterInput = false;
    currentDate = new Date();
    specificationArray: Specification[];
    equipamentArray: Equipament[];
    time;
    value;
    todayDate;
    inputReadonly = false;
    innerValue: Date = new Date();
    icons: any = [
      {
        id: "0",
        icon: ""
      },
      {
        id: "1",
        icon: "arrow_forward"
      },
      {
        id: "2",
        icon: "arrow_back"
      },
      {
        id: "3",
        icon: "swap_horiz"
      }
    ];

    constructor(private calendarService: CalendarService,
                public dialog: MatDialog,
                private specificationSerivce: SpecificationsService,
                private equipamentService: EquipamentsService,
                private toastrService: ToastrService) {
      this.time = moment();
    }

    ngAfterViewInit(): void {
      this.ajusteCSS();
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
      this.calendarService.getCalendarByDay(date).subscribe((resp: any) => {
        this.dataSource = resp;
      });
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

    openDialogTechnique(element: Calendar){
      let isDriver = false;
      const dialogRef = this.dialog.open(PersonDialogUpdateComponent, {
        width: '400px',
        height: '250px',
        disableClose: true,
        data: {element, isDriver}
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result === undefined)
          return;
        
        this.getCalendars();           
      });
    }

    openDialogDriver(element: Calendar){
      let isDriver = true;
      const dialogRef = this.dialog.open(PersonDialogUpdateComponent, {
        width: '400px',
        height: '250px',
        disableClose: true,
        data: {element, isDriver}
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

    showClientCity(item){
      let ret = [];
      if (item.noCadastre){
        ret.push(item.temporaryName);
      }else{
        let split = item.client.name.split(' ');
        ret.push(split[0]);
        ret.push(item.client.city.nome)
      }
      
      if (item.calendarSpecifications.filter(x => x.active).length > 0){
        ret.push(this.descriptionSpecifications(item));
      }

      return ret.join(' - ');
    }

    showIconTravelOn(value): string {
      let ret = '';
      switch(value){
        case 1:
          ret = 'arrow_forward';
          break;
        case 2:
          ret = 'arrow_back';
          break;
        case 3:
          ret = 'swap_horiz';
          break;
      }
      return ret;
;    }

    statusToString(status): string{
      let ret = 'Confirmada';
      switch (status){
        case '2':
          ret = 'Pendente';
          break;
        case '3':
          ret = 'Cancelada';
          break;
        case '4':
          ret = 'Excluida';
          break;
        case '5':
          ret = 'Pre-Agendada'
          break;
      }

      return ret;
    }

    ajusteCSS(): void {
      document
            .querySelectorAll<HTMLElement>('.header__title-button-icon')
            .forEach(node => node.click())
    }
  }