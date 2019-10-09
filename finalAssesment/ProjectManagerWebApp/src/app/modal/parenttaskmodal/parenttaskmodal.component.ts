import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {ParentTasks} from "../../shared/models/parenttask.model";
import { TasksService } from '../../services/tasks.service';

@Component({
  selector: 'app-parenttaskmodal',
  templateUrl: './parenttaskmodal.component.html',
  styleUrls: ['./parenttaskmodal.component.css'],
  providers: [TasksService] 
})
export class ParenttaskmodalComponent implements OnInit {

 
  @Input() public parenttask:any;
  @Output() passEntry: EventEmitter<any> = new EventEmitter();
  parenttaskItem:ParentTasks= new ParentTasks();
  parenttaskList:ParentTasks[];
  sortstring:string="Parent_Task";
  selectedValue:string;
  error:any={isError:false,errorMessage:''};

  constructor( public activeModal: NgbActiveModal,private tasksService: TasksService) {
    
    
   }

  ngOnInit() {
    console.log(this.parenttask);
    this.selectedValue = this.parenttask && this.parenttask.parentid==0?"":this.parenttask.parentid; 
    this.sortstring ="Parent_Task";
    this.getParentTasksList();
  }

  onChange(event) {
   this.error={isError:false,errorMessage:''};
    let name = event.target.options[event.target.options.selectedIndex].text;

    this.parenttask.ParentTask = name;
    this.parenttask.parentid = this.selectedValue;
    console.log(this.parenttask);
}
public deleteAlert(){

  this.error.isError=false;
  this.error.errorMessage="";
}
  passBack(type:any) {
    //console.log(FirstName);
    //this.parenttask.Fullname =
     //console.log(document.querySelector("parenttask"));
     //console.log('seelctor');
    //this.parenttask.Parent_ID = this.selectedValue;
   //if(type==1){
      if(this.parenttask.parentid>0 && this.parenttask.ParentTask!=""){
        this.passEntry.emit(this.parenttask);
        this.activeModal.close(this.parenttask);
      }
      else{
      this.error.isError=true;
      this.error.errorMessage="Please select a parenttask";
      }
 //   }
 
  }

  getParentTasksList(): void {
    this.tasksService.GetParentTasksList()
        .subscribe(parenttask => {

          console.log('parenttask=')
          console.log(parenttask);
          this.parenttaskList = parenttask;
          console.log('this.parenttask=' + this.parenttaskList);
                    console.log('this.parenttask.length=' + this.parenttaskList.length);
                    console.log('this.parenttask[0].ParentTask=' + this.parenttaskList[0].ParentTask);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });
  }
}