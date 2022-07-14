import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Log } from '../models/Log';

const httpOptions = {
  headers: new HttpHeaders({
    'content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class LogService {

  url = `${environment.baseUrl}/api/logs`;

constructor(private http: HttpClient) { }

getAll(): Observable<Log[]>{
  return this.http.get<Log[]>(`${this.url}`);
}

getById(id: number): Observable<Log>{
  return this.http.get<Log>(`${this.url}/${id}`);
}

create(log: Log):Observable<any>{
  return this.http.post<Log>(this.url, Log, httpOptions);
}

// Delete(logId: Number): Observable<any>{
//   return this.http.delete<Number>(`${this.url}/${logId}`, httpOptions);
// }

}
