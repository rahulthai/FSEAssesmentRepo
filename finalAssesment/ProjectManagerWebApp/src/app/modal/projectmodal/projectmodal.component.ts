import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {Projects} from "../../shared/models/projects.model";
import { ProjectsService } from '../../services/projects.service';
import { FilterPipe } from '../../shared/filter.pipe';

@Component({
  selector: 'app-projectmodal',
  templateUrl: './projectmodal.component.html',
  styleUrls: ['./projectmodal.component.css'],
  providers: [ProjectsService] 
})
export class ProjectmodalComponent implements OnInit {

  @Input() public project:any;
  @Output() passEntry: EventEmitter<any> = new EventEmitter();
  projectItem:Projects= new Projects();
  projectsList:Projects[];
  sortstring:string="Project";
  selectedValue:string;
  error:any={isError:false,errorMessage:''};
  searchText:string;
  constructor( public activeModal: NgbActiveModal,private projectsService: ProjectsService) {
    
    
   }

  ngOnInit() {
    console.log(this.project);
    this.selectedValue = this.project && this.project.projectid==0?"":this.project.projectid; 
    this.sortstring ="Project";
    this.getProjectsList();
  }

  onChange(event) {
   this.error={isError:false,errorMessage:''};
    let name = event.target.options[event.target.options.selectedIndex].text;

    this.project.Project = name;
    this.project.projectid = this.selectedValue;
    console.log(this.project);
}
public deleteAlert(){

  this.error.isError=false;
  this.error.errorMessage="";
}
  passBack(type:any) {
    //console.log(FirstName);
    //this.project.Fullname =
     //console.log(document.querySelector("project"));
     //console.log('seelctor');
    //this.project.projectid = this.selectedValue;
   //if(type==1){
      if(this.project.projectid>0 && this.project.Project!=""){
        this.passEntry.emit(this.project);
        this.activeModal.close(this.project);
      }
      else{
      this.error.isError=true;
      this.error.errorMessage="Please select a project";
      }
 //   }
 
  }

  getProjectsList(): void {
    this.projectsService.getProjectsList()
        .subscribe(projects => {

          console.log('projects=')
          console.log(projects);
          this.projectsList = projects;
          console.log('this.projects=' + this.projectsList);
                    console.log('this.projects.length=' + this.projectsList.length);
                    console.log('this.projects[0].Project=' + this.projectsList[0].Project);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }
}