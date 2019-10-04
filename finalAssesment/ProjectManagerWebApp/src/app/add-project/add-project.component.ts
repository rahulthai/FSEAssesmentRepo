import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReactiveFormsModule, NgForm, FormGroup, FormControl } from '@angular/forms';
import { UsermodalComponent } from '../modal/usermodal/usermodal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {Projects} from "../shared/models/projects.model";
import {ProjectsService} from "../services/projects.service";
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
 })
export class AddProjectComponent implements OnInit {

  userObj = {
    Fullname: '',
    userid: 0
  }

  @Input() project : Projects=new Projects();
  @Output() onAddedProject = new EventEmitter<boolean>();
  //addProjectForm: FormGroup;
  checkbox: boolean;
  enablefields:boolean;
  //currentDate: Date;
  //today:Date;
  error:any={isError:false,errorMessage:''};
  //form : NgForm

  constructor(public modalService: NgbModal,
              private projectsService: ProjectsService) { 
    console.log("constructor project");
    console.log(this.project);
    this.checkbox=true;
    this.enablefields= true;
  }

  ngOnInit() {
console.log('init event');
console.log(this.project);
    if(this.project.Project_ID<=0 || this.project.Project_ID==undefined){

        //this.currentDate = new Date();
        //this.today = new Date();
        
        this.project.Project="";
        if(this.checkbox){
          this.project.StartDate =  new Date();
          this.project.EndDate = this.addDays( new Date() , 1);
        }
        this.project.Priority="0";
        this.project.name="";

    }
   
  }

  createProjectFormGroup() {
    return new FormGroup({
        Project: new FormControl(),
        StartDate: new FormControl(),
        EndDate: new FormControl(),
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
          this.enablefields= true;
         
          if(this.project.Project_ID==undefined || this.project.Project_ID<=0){
            this.project.StartDate =  new Date();
            this.project.EndDate = this.addDays( new Date() , 1);
          }

        }
        else{
          this.enablefields=false;

          if(this.project.Project_ID==undefined || this.project.Project_ID<=0){
            this.project.StartDate =  null;
            this.project.EndDate = null;
          }
        }
    }


  onSubmit(form:NgForm){
    this.error={isError:false,errorMessage:''};
    console.log('onsubmit');
    console.log(this.project);
    if(form.valid){
      if(this.checkbox){
        this.compareTwoDates();
      }
      if(this.error.isError){
        return;
      }
      else if(parseInt(this.project.Priority)<=0){
        this.error={isError:true,errorMessage:"Set Priority from given range"};
        return;
      }
      else if(this.project.User_ID==undefined || this.project.User_ID<=0){
        this.error={isError:true,errorMessage:"Assign a user to project"};
        return;
      }
      else{
        console.log('form validated');
        console.log(form.value);
        console.log(this.project);
        if(this.project.Project_ID==undefined || this.project.Project_ID<=0){ 
        this.addProject(this.project);
        }else{
        this.updateProject(this.project);

        }
      }
   //this.viewUsersComponent.getUsersList();
  
    }
    else
    {
      console.log('form not valid');
      return;
    }
  }


  compareTwoDates(){
    if(new Date(this.project.EndDate)<new Date(this.project.StartDate)){
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

    this.project = new Projects();
  }

  addProject(item:Projects): void {
    let that= this;
    this.projectsService.addProject(item)
        .subscribe(projects => {projects;
          console.log('project added successfully')
          console.log(that.project);
          that.project = new Projects();
          that.project.Priority="0";
          that.checkbox=true;
          that.project.StartDate =  new Date();
          that.project.EndDate = that.addDays( new Date() , 1);
          console.log(that.project);
          this.onAddedProject.emit();
        });
  }

  updateProject(item:Projects): void {
    let that= this;
    this.projectsService.updateProject(item)
        .subscribe(projects => {projects;
          console.log('project updated successfully')
          console.log(that.project);
          that.project = new Projects();
          that.project.Priority="0";
          that.checkbox=true;
          that.project.StartDate =  new Date();
          that.project.EndDate = that.addDays( new Date() , 1);
          console.log(that.project);
          this.onAddedProject.emit();
        });
  }

  openModal() {
console.log('this.userObj');
console.log(this.userObj);
    const modalRef = this.modalService.open(UsermodalComponent);
    modalRef.componentInstance.user = this.userObj;
    modalRef.result.then((result) => {
      if (result) {
        this.project.name = result.Fullname;
        this.project.User_ID = result.userid;
        console.log("result");
        console.log(result);

      }
    });
  }

  setPriority(event){
    //console.log(this.project.Priority)
    //console.log(event.target.value)
    this.project.Priority = event.target.value;
    //console.log(this.project.Priority)

  }

  deleteAlert(index){

    this.error.isError=false;
    this.error.errorMessage="";
  }
}
