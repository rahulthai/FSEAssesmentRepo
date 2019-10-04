import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReactiveFormsModule, NgForm, FormGroup, FormControl } from '@angular/forms';
import { UsermodalComponent } from '../modal/usermodal/usermodal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {Tasks} from "../shared/models/tasks.model";
import {TasksService} from "../services/tasks.service";
import { DatePipe } from '@angular/common';
import { ProjectmodalComponent } from '../modal/projectmodal/projectmodal.component';
import { ParenttaskmodalComponent } from '../modal/parenttaskmodal/parenttaskmodal.component';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {

  userObj = {
    Fullname: '',
    userid: 0
  }

  projectObj = {
    Project: '',
    projectid: 0
  }

  parenttaskObj = {
    ParentTask: '',
    parentid: 0
  }


  @Input() task : Tasks=new Tasks();
  @Output() onAddedTask = new EventEmitter<boolean>();
  @Output() onHideEditTask = new EventEmitter<boolean>();
  @Input() checkbox: boolean;
  @Input() enablefields:boolean;
  @Input() showaddtasks :boolean;
  @Input() showreset :boolean;
  @Input() screentype :string;
  error:any={isError:false,errorMessage:''};

  constructor(public modalService: NgbModal,
    private tasksService: TasksService) {
      
    console.log("constructor add task");
    console.log(this.task);
    this.checkbox=false;
    this.enablefields= true;
  }

  ngOnInit() {
    console.log('init event');
    console.log(this.task);
        if(this.task.Task_ID<=0 || this.task.Task_ID==undefined){
    
            //this.currentDate = new Date();
            //this.today = new Date();
            
            this.task.Task="";
            if(!this.checkbox){
              this.task.Start_Date =  new Date();
              this.task.End_Date = this.addDays( new Date() , 1);
            }
            this.task.Priority="0";
            this.task.Name="";
    
        }
       
      }
    
      createTaskFormGroup() {
        return new FormGroup({
            Task: new FormControl(),
            Start_Date: new FormControl(),
            End_Date: new FormControl(),
            Priority: new FormControl(),
            Name: new FormControl()
           })
      }
    
    
      addDays(date: Date, days: number): Date {
        console.log('adding ' + days + ' days');
        console.log(date);
        date.setDate(date.getDate() + days);
        console.log(date);
        return date;
      }
      onCheckboxChange() {
        console.log('oncheckboxchange');
        console.log(this.checkbox);
            //this.checkbox = !this.checkbox;
            if(this.checkbox==true){
              this.enablefields= false;
              if(this.task.Task_ID==undefined || this.task.Task_ID<=0){
                this.task.Start_Date =  null;
                this.task.End_Date = null;
                this.task.Priority = "0";
              }
              this.task.IsParentTask = true;
            }
            else{
              this.enablefields=true;
              if(this.task.Task_ID==undefined || this.task.Task_ID<=0){
                this.task.Start_Date =  new Date();
                this.task.End_Date = this.addDays( new Date() , 1);
              }
              this.task.IsParentTask = false;
            }
        }
    
      onSubmit(form:NgForm){
        this.error={isError:false,errorMessage:''};
        console.log('onsubmit');
        console.log(this.task);
        //debugger; 
        if(form.valid){
          if(this.task.Project_ID==undefined || this.task.Project_ID<=0){
            this.error={isError:true,errorMessage:"Select a Project"};
            return;
          }
          if(!this.checkbox){
              
              if(parseInt(this.task.Priority)<=0){
                this.error={isError:true,errorMessage:"Set Priority from given range"};
                return;
              }
              else if(this.task.Parent_ID==undefined || this.task.Parent_ID<=0){
                this.error={isError:true,errorMessage:"Select a Parent Task"};
                return;
              }
              else if(new Date(this.task.End_Date)<new Date(this.task.Start_Date)){
              this.error={isError:true,errorMessage:"End Date can't be before Start Date"};
                  return;
              }
              else if(this.task.User_ID==undefined || this.task.User_ID<=0){
                this.error={isError:true,errorMessage:"Assign a user to task"};
                return;
              }
          }
            console.log('form validated');
            console.log(form.value);
            console.log(this.task);
            if(this.task.Task_ID==undefined || this.task.Task_ID<=0){   
            this.addTask(this.task);
            }
            else{
              this.updateTask(this.task);
            }
        }
        else
        {
          console.log('form not valid');
          return;
        }
      }
    
    
      compareTwoDates(){
        if(new Date(this.task.End_Date)<new Date(this.task.Start_Date)){
           this.error={isError:true,errorMessage:"End Date can't be before Start Date"};
        }
     }
    
      updateStartDateModel(value:string)
      {
    console.log(value);
      }
    
      updateEndDateModel(value:string)
      {
    console.log(value);
        
      }
    
      resetModel(){
    
        this.task = new Tasks();
        this.task.Priority="0";
      }
    
      addTask(item:Tasks): void {
        let that= this;
        this.tasksService.addTask(item)
            .subscribe(tasks => {tasks;
              console.log('task added successfully')
              console.log(that.task);
              that.task = new Tasks();
              that.task.Priority="0";
              that.checkbox=true;
              that.task.Start_Date =  new Date();
              that.task.End_Date = that.addDays( new Date() , 1);
              console.log(that.task);
              
              this.onAddedTask.emit();
            });
      }
    
      updateTask(item:Tasks): void {
        let that= this;
        this.tasksService.updateTask(item)
            .subscribe(tasks => {tasks;
              console.log('task updated successfully')
              console.log(that.task);
              that.task = new Tasks();
              that.task.Priority="0";
              that.checkbox=true;
              that.task.Start_Date =  new Date();
              that.task.End_Date = that.addDays( new Date() , 1);
              console.log(that.task);
              
              this.onAddedTask.emit();
            });
      }
    

  openModal(type:any) {
        if(type==1){
          console.log('this.projectObj');
          console.log(this.projectObj);
          const modalProjRef = this.modalService.open(ProjectmodalComponent);
          modalProjRef.componentInstance.project = this.projectObj;
          modalProjRef.result.then((result) => {
          if (result) {
            this.task.Project = result.Project;
            this.task.Project_ID = result.projectid;
            console.log("result");
            console.log(result);
    
          }
        });
        }
        
        else if(type==2){
          console.log('this.parenttaskObj');
    console.log(this.parenttaskObj);
        const modalParentTaskRef = this.modalService.open(ParenttaskmodalComponent);
        modalParentTaskRef.componentInstance.parenttask = this.parenttaskObj;
        modalParentTaskRef.result.then((result) => {
          if (result) {
            this.task.ParentTask = result.ParentTask;
            this.task.Parent_ID = result.parentid;
            console.log("result");
            console.log(result);
    
          }
        });
        }
        else if(type==3){
          console.log('this.userObj');
          console.log(this.userObj);
        const modalRef = this.modalService.open(UsermodalComponent);
        modalRef.componentInstance.user = this.userObj;
        modalRef.result.then((result) => {
          if (result) {
            this.task.Name = result.Fullname;
            this.task.User_ID = result.userid;
            console.log("result");
            console.log(result);
    
          }
        });
        }

    
      }
    
      setPriority(event){
        //console.log(this.task.Priority)
        //console.log(event.target.value)
        this.task.Priority = event.target.value;
        //console.log(this.task.Priority)
    
      }
    
      deleteAlert(index){
    
        this.error.isError=false;
        this.error.errorMessage="";
      }

      hideModel(){
        //console.log('this.showaddtasks '+ this.showaddtasks);
        //console.log('this.showreset '+ this.showreset);
        console.log('this.screentype '+this.screentype);
        if(this.screentype=="view"){
           //this.showaddtasks=false;
          // this.showreset=false;
          //console.log('calling hideedittask');
          this.onHideEditTask.emit();
        }
        else{
          this.showaddtasks=true;
          this.showreset=true;
        }

      }

}
