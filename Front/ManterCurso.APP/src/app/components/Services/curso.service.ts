import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Curso } from '../models/Curso';

const httpOptions = {
  headers: new HttpHeaders({
    'content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root' // a instancia vai ser fornecida na raiz do projeto
})
export class CursoService {

private readonly url = `${environment.baseUrl}/api/cursos`; //caminho do controller

constructor(private http: HttpClient) { }

getAllStatusValido(): Observable<Curso[]>{
  return this.http.get<Curso[]>(`${this.url}/statusValido`);
}

getById(id: Number): Observable<Curso>{
  return this.http.get<Curso>(`${this.url}/${id}`);
}

create(curso: Curso):Observable<any>{
  return this.http.post<Curso>(this.url, curso, httpOptions);
}

delete(cursoId: Number): Observable<any>{
  return this.http.delete<Number>(`${this.url}/${cursoId}`, httpOptions);
}

delecaoLogica(cursoId : Number) : Observable<any>{
  return this.http.put<Number>(`${this.url}/delete/${cursoId}`,  httpOptions);
}

update(curso: Curso) : Observable<any>{
  return this.http.put<Curso>(`${this.url}/${curso.cursoId}`, curso, httpOptions);
}

}
