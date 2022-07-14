import { Component, OnInit } from '@angular/core';
import { Log } from '../models/Log';
import { LogService } from '../Services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {

  public logs: Log[];

  displayedColumns = [
    'curso',
    'dataInclusao',
    'dataAtualizacao',
    'usuario'

  ]

  constructor(private logservice : LogService) { }

  ngOnInit(): void {
    this.logservice.getAll().subscribe(resultado =>{
      this.logs = resultado;
    })
  }

}
