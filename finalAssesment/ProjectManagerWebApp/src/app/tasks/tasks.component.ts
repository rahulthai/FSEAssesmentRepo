import { Component, OnInit } from '@angular/core';
import {Tasks} from "../shared/models/tasks.model";
import { TasksService } from '../services/tasks.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
  providers: [TasksService] 
})
export class TasksComponent implements OnInit {

  taskid;
  taskItem:Tasks= new Tasks();
  tasksList:Tasks[];
  ischeckbox :boolean=false;
  isenablefields:boolean=true;
  showaddflag:boolean=true;
  showresetflag:boolean=true;
  screen :string ="add";
  constructor(private tasksService: TasksService) { }

  ngOnInit() {
    this.getTasksList(false);
  }

  getTaskByID(event){
    console.log('this is parent '+event)
    this.taskid = event;
    this.loadTaskData(this.taskid);
  }

  loadTaskData(id:number){
    var that = this;
    
     this.tasksService.getTaskByID(id)
         .subscribe(u =>  {that.taskItem =u;
          console.log("this.task inside");
            
          // that.task.Task_ID=u.Task_ID;
          //   that.task.FirstName=u.FirstName;
          //   that.task.LastName=u.LastName;
          //   that.task.Employee_ID=u.Employee_ID;
          //this.ngOnInit();
         
          if(that.taskItem.Parent_ID==null || 
                that.taskItem.Parent_ID==undefined  
                ||that.taskItem.Parent_ID<=0){ 
                  that.taskItem.IsParentTask = true;
                  that.taskItem.Priority = that.taskItem.Priority!=null ? that.taskItem.Priority :"0";
                  that.ischeckbox = true;
                  that.isenablefields = false;
                }
                else{
                  that.taskItem.IsParentTask = false;
                  that.ischeckbox = false;
                  that.isenablefields = true;
                  that.taskItem.Priority = that.taskItem.Priority!=null ? that.taskItem.Priority :"0";


                } 
          console.log(that.taskItem);
         // console.log('form value');
        //console.log(that.addTaskForm.value);
        });
//console.log("this.task after");
//console.log(this.task);
 
  }

  public getTasksList(param:any): void {
    this.tasksService.getTasksList()
        .subscribe(tasks => {
          console.log('tasks=')
          console.log(tasks);
          this.tasksList = tasks;
          console.log('this.tasks=' + this.tasksList);
                    console.log('this.tasks.length=' + this.tasksList.length);
                    console.log('this.tasks[0].FirstName=' + this.tasksList[0].Task);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }

   deleteTask(id:any): void {
     this.tasksService.deleteTask(id)
         .subscribe(tasks => {
          console.log('Task deleted'); 
          this.getTasksList(false);
         });
   }

}
