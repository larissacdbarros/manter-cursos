import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCursoComponent } from './components/cursos/addCurso/addCurso.component';
import { CursosComponent } from './components/cursos/cursos.component';
import { DeleteCursoComponent } from './components/cursos/deleteCurso/deleteCurso.component';
import { LogComponent } from './components/log/log.component';

const routes: Routes = [
  { path:"cursos", component: CursosComponent },
  {path:"addCurso", component: AddCursoComponent},
  {path:"deleteCurso", component: DeleteCursoComponent},
  {path:"log", component: LogComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
