import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { CategoriaCurso } from '../models/CategoriaCurso';

const httpOptions = {
  headers: new HttpHeaders({
    'content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CategoriaCursoService {

  url = `${environment.baseUrl}/api/categoriasCursos`;

constructor(private http: HttpClient) { }

getAll(): Observable<CategoriaCurso[]>{
  return this.http.get<CategoriaCurso[]>(`${this.url}`);
}

getById(id: number): Observable<CategoriaCurso>{
  return this.http.get<CategoriaCurso>(`${this.url}/${id}`);
}

}
