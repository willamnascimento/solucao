import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Calendar } from '../models/calendar';

const URL_CALENDARS = '/api/v1/calendar';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  
  constructor(private http: HttpClient){

  }

  getCalendarByDay(date: string): Observable<Calendar[]>{
    return this.http.get(`${environment.URL_API}${URL_CALENDARS}/?date=${date}`)
    .pipe(map((resp: Calendar[]) => {
      return resp;
    }));
  }

  save(calendar: Calendar): Observable<Calendar>{
    return this.http.post(`${environment.URL_API}${URL_CALENDARS}`,calendar)
    .pipe(map((resp: Calendar) => {
      return resp;
    }));
  }

  update(calendar: Calendar): Observable<Calendar>{
    return this.http.put(`${environment.URL_API}${URL_CALENDARS}/${calendar.id}`,calendar)
    .pipe(map((resp: Calendar) => {
      return resp;
    }));
  }
  
  availability(startDate: string, endDate: string, clientId: string, equipamentId: string): Observable<Calendar[]>{
    return this.http.get(`${environment.URL_API}${URL_CALENDARS}/availability?startDate=${startDate}&endDate=${endDate}&clientId=${clientId}&equipamentId=${equipamentId}`)
    .pipe(map((resp: Calendar[]) => {
      return resp;
    }));
  }
}


