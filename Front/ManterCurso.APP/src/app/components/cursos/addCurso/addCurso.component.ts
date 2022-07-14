import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CategoriaCurso } from '../../models/CategoriaCurso';
import { Curso } from '../../models/Curso';
import { CategoriaCursoService } from '../../Services/categoriaCurso.service';
import { CursoService } from '../../Services/curso.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-addCurso',
  templateUrl: './addCurso.component.html',
  styleUrls: ['./addCurso.component.css']
})
export class AddCursoComponent implements OnInit {

  public titulo: string;
  public categoriasCurso: CategoriaCurso[];
  public submitted = false;
  public cursoId: Number;


  formulario = this.fb.group({
    cursoId: [null],
    descricao: ['', Validators.required],
    dataInicio: ['', Validators.required],
    dataTermino: ['', Validators.required],
    qtdAlunosTurma: [null],
    categoriaCursoId: ['', Validators.required]
  });



  constructor(private fb: FormBuilder,
              private cursoService: CursoService,
              private categoriaCursoService : CategoriaCursoService,
              private toastr: ToastrService,
              public dialogRef: MatDialogRef<AddCursoComponent>,
              @Inject(MAT_DIALOG_DATA) public curso: Curso ) { }

  ngOnInit() {
    this.cursoId = Number (localStorage.getItem('cursoId'));

    if(this.curso !== null && this.curso.cursoId !== null){
      this.cursoService.getById(this.curso.cursoId).subscribe(resultado => {
        this.formulario.controls.cursoId.setValue(resultado.cursoId);
        this.formulario.controls.descricao.setValue(resultado.descricao);
        this.formulario.controls.dataInicio.setValue(new Date(resultado.dataInicio).toISOString().split('T')[0]);
        this.formulario.controls.dataTermino.setValue(new Date(resultado.dataTermino).toISOString().split('T')[0]);
        this.formulario.controls.qtdAlunosTurma.setValue(resultado.qtdAlunosTurma);
        this.formulario.controls.categoriaCursoId.setValue(resultado.categoriaCurso.categoriaCursoId);
      })

      this.titulo = 'Atualizar Curso';

    }else{
      this.titulo = 'Novo Curso';
    }
    this.comboBoxCategoria();


  }

  adicionar(): void{
     this.submitted = true;
    if(this.formulario.valid){

      const curso: Curso = this.formulario.value;
      console.log(this.formulario.controls.cursoId.value)
      if(this.formulario.controls.cursoId.value !== null){
        console.log('update')
        this.cursoService.update(curso).subscribe(resultado => {
          this.toastr.success('Curso atualizado com sucesso!')
          this.dialogRef.close()}, (error) => {

            this.toastr.error(error.error.title)
          });

      }else{
        console.log('create')
        this.cursoService.create(curso).subscribe((resultado) =>{
          this.toastr.success('Curso adicionado com sucesso!')
          this.dialogRef.close()}, (error) => {

            this.toastr.error(error.error.title)
          });
      }
    }

    }

  cancelar(): void{
    this.dialogRef.close();
  }

  comboBoxCategoria(){
    this.categoriaCursoService.getAll().subscribe(resultado => {
      this.categoriasCurso = resultado;
    });

  }

  get formularioControls() {
    return this.formulario.controls;
  }

}
