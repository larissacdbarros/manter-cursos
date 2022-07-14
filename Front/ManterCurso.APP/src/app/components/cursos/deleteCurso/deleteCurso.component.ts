import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Curso } from '../../models/Curso';
import { CursoService } from '../../Services/curso.service';
import { CursosComponent } from '../cursos.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-deleteCurso',
  templateUrl: './deleteCurso.component.html',
  styleUrls: ['./deleteCurso.component.css']
})
export class DeleteCursoComponent implements OnInit {

  public cursos : Curso;

  constructor(private cursoService : CursoService,
              private toastr: ToastrService,
              @Inject(MAT_DIALOG_DATA) public curso: Curso,
              public dialogRef: MatDialogRef<DeleteCursoComponent>,
              private dialog : MatDialog) { }

  ngOnInit() {
  }

  apagar(id){

    // const cursoId: Number = this.curso.status;
    this.cursoService.delecaoLogica(id).subscribe(resultado => {
      this.toastr.success('Curso deletado com sucesso!')
      this.dialogRef.close()
    }, (error) => {
      this.toastr.error(error.error.title)
    });
  }

  voltar(): void{
    this.dialogRef.close();
  }

}
