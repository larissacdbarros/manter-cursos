import { Component, OnInit, ViewChild } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable } from 'rxjs';
import { CategoriaCurso } from '../models/CategoriaCurso';
import { Curso } from '../models/Curso';
import { CursoService } from '../Services/curso.service';
import { AddCursoComponent } from './addCurso/addCurso.component';
import { DeleteCursoComponent } from './deleteCurso/deleteCurso.component';


@Component({
  selector: 'app-cursos',
  templateUrl: './cursos.component.html',
  styleUrls: ['./cursos.component.css']
})
export class CursosComponent implements OnInit {

  public cursos: Curso[];
  public cursoId: Number;

  queryfield = new FormControl();

  
  displayedColumns = [
  'descricao',
   'dataInicio',
   'dataTermino',
   'qtdAlunosTurma',
   'categoria',
   'acoes'
  ];

  formGroupPesquisa: FormGroup;


  constructor(private cursoService: CursoService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private dialog : MatDialog ) {

    // pode ser dentro do ng on init tbm
   }

  ngOnInit(): void {
   this.listarCursos()

  }

  listarCursos(){

    this.cursoService.getAllStatusValido().subscribe(resultado => {
      this.cursos = resultado;
    });

    this.formGroupPesquisa = this.formBuilder.group({
      descricao: [null],
    });

    const query = new Map();
    if(this.formGroupPesquisa.value.descricao){
      query.set("nome_like", this.formGroupPesquisa.value.descricao)
    }

  }

  openDialogAdd(){
    const dialogRef =  this.dialog.open(AddCursoComponent);
    dialogRef.afterClosed().subscribe(result => {
      this.cursoService.getAllStatusValido().subscribe(resultado => {
        this.cursos = resultado;
      });
    })

  }

  openDialogDelete(id: Number){
    const dialogRef = this.dialog.open(DeleteCursoComponent, {
      data : {cursoId : id},
      width: '600px'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.cursoService.getAllStatusValido().subscribe(resultado => {

        this.cursos = resultado;
      }, (error) => {
        this.toastr.error(error.error.title)
      });

    })
  }

  openDialogUpdate(id: Number){
    const dialogRef =  this.dialog.open(AddCursoComponent,
      {data: {cursoId: id}});
    dialogRef.afterClosed().subscribe(result => {
      this.cursoService.getAllStatusValido().subscribe(resultado => {
        this.cursos = resultado;
      });
    })
  }

  limparPesquisa(){
    this.formGroupPesquisa.reset();
    this.listarCursos();

  }



}
