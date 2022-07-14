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




  formGroupPesquisa = this.formBuilder.group({
    descricao: [null],
    dataInicio: [null],
    dataTermino: [null]
  });


  displayedColumns = [
  'descricao',
   'dataInicio',
   'dataTermino',
   'qtdAlunosTurma',
   'categoria',
   'acoes'
  ];



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

  pesquisar(){

    var descricao = this.formGroupPesquisa.controls.descricao.value;

    if(descricao && descricao.trim() !== ''){
      descricao = descricao.trim();
    }

    this.cursoService.getAllStatusValido(descricao).subscribe(resultado => {
      this.cursos = resultado;
    });

    var dataInicio = this.formGroupPesquisa.controls.dataInicio.value;
    var dataTermino = this.formGroupPesquisa.controls.dataTermino.value;

    if((dataInicio && dataInicio.trim() !== '') || (dataTermino && dataTermino.trim() !== '') ){
      dataInicio = dataInicio.trim();
      dataTermino = dataTermino.trim();
    }

    this.cursoService.getAllStatusValido(descricao, dataInicio, dataTermino).subscribe(resultado => {
      this.cursos = resultado;
    });

  }
}
