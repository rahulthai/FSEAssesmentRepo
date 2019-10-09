import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { CommonModule } from "@angular/common";
import {Router, ActivatedRoute} from '@angular/router';
import { FilterPipe } from '../shared/filter.pipe';
import {Tasks} from "../shared/models/tasks.model";
import { TasksService } from '../services/tasks.service';
import { Observable } from 'rxjs'
import { OrderPipe } from 'ngx-order-pipe';
import { ProjectmodalComponent } from '../modal/projectmodal/projectmodal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { interval as observableInterval } from "rxjs";
import { takeWhile, scan, tap } from "rxjs/operators";

@Component({
  selector: 'app-view-tasks',
  templateUrl: './view-tasks.component.html',
  styleUrls: ['./view-tasks.component.css'],
  providers: [TasksService,OrderPipe] 
})
export class ViewTasksComponent implements OnInit {
  taskItem:Tasks=new Tasks();
  //@Input() addUserComponent: AddUserComponent;
  @Input() tasks : Tasks[];
  @Output() onGetTaskByID = new EventEmitter<boolean>(); 
  @Output() onDeleteTask = new EventEmitter<boolean>(); 
  projectId:number;
  project:string;
  sortstring :string ="Task";
  reverse: boolean = true;
  sortedCollection: any[];
  searchText : string;
  
  projectObj = {
    Project: '',
    projectid: 0
  }

  constructor(public modalService: NgbModal,private tasksService: TasksService,private orderPipe: OrderPipe,private route: ActivatedRoute) { 

    this.sortedCollection = orderPipe.transform(this.tasks, "Task");
    console.log(this.sortedCollection);
  }  

  ngOnInit() {
    console.log('view task init');
  }
  getTaskByID(id:any,el:any): void {
    //window.scroll(0,0);
   this.scrollToTop(el);
    this.onGetTaskByID.emit(id);
    console.log(el);

  }

   getTasksListByProject(): void {
    this.tasksService.getTasksListByProject(this.projectId)
        .subscribe(tasklst => {
          console.log('tasks=')
          console.log(tasklst);
          this.tasks = tasklst;
          console.log('this.tasks=' + this.tasks);
                    console.log('this.tasks.length=' + this.tasks.length);
                    console.log('this.tasks[0].FirstName=' + this.tasks[0].Task);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }

   editTask(id:any,item:Tasks): void {
    this.tasksService.getTaskByID(id)
        .subscribe(tasks => this.taskItem = tasks);
   }

   deleteTask(id:any,task:Tasks):void{
    if(confirm("Are you sure to mark Task - '"+task.Task+"' Completed?")) {
      this.onDeleteTask.emit(id);
    }
   }

   sortresult(sort:string){

    this.sortstring=sort;
    console.log("search criteria "+this.sortstring);
   }

   openModal(type:any) {
      console.log('this.projectObj');
      console.log(this.projectObj);
      const modalProjRef = this.modalService.open(ProjectmodalComponent);
      modalProjRef.componentInstance.project = this.projectObj;
      modalProjRef.result.then((result) => {
      if (result) {
        this.project = result.Project;
        this.projectId = result.projectid;
        this.getTasksListByProject();
        console.log("result");
        console.log(result);

      }
    });
  }

  scrollToTop(el) {
    let scrollToTop = window.setInterval(() => {
      let pos = window.pageYOffset;
      if (pos > 0) {
          window.scrollTo(0, pos - 20); // how far to scroll on each step
      } else {
          window.clearInterval(scrollToTop);
      }
  }, 16);    
  }

}
