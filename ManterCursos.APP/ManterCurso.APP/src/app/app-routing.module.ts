import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursosComponent } from './components/cursos/cursos.component';
import { LogComponent } from './components/log/log.component';

const routes: Routes = [
  {
    path:"cursos", component: CursosComponent
  },
  {
    path:"log", component: LogComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
