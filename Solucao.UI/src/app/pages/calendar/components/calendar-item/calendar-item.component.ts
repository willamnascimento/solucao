import { SpecificationsService } from './../../../../shared/services/specifications.service';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Calendar } from 'src/app/shared/models/calendar';
import moment from 'moment';
import { Specification } from 'src/app/shared/models/specification';

@Component({
    selector: 'app-calendar-item',
    templateUrl: 'calendar-item.component.html',
    styleUrls: ['./calendar-item.component.scss']
  })
  export class CalendarItemComponent implements OnChanges{
    
    @Input() list: Calendar[];
    @Input() specificationArray: Specification[];

    constructor(private specificationSerivce: SpecificationsService) {
      
    }

    ngOnChanges(changes: SimpleChanges): void {
        console.log(this.list);
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

  }