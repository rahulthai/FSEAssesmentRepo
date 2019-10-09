import { Component, OnInit, EventEmitter } from '@angular/core';
import {Projects} from "../shared/models/projects.model";
import { ProjectsService } from '../services/projects.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css'],
  providers: [ProjectsService] 

})
export class ProjectsComponent implements OnInit {

  projectid;
  projectItem:Projects= new Projects();
  projectsList:Projects[];
  constructor(private projectsService: ProjectsService) { }

  ngOnInit() {
    //this.projectItem.Project ="test from parent";
    
    this.getProjectsList(false);
  }

  getProjectByID(event){
    console.log('this is parent '+event)
    this.projectid = event;
    this.loadProjectData(this.projectid);
  }

  loadProjectData(id:number){
    var that = this;
    
     this.projectsService.getProjectByID(id)
         .subscribe(u =>  {that.projectItem =u;
          console.log("this.project inside");
            
          // that.project.Project_ID=u.Project_ID;
          //   that.project.FirstName=u.FirstName;
          //   that.project.LastName=u.LastName;
          //   that.project.Employee_ID=u.Employee_ID;
          //this.ngOnInit();
          console.log(that.projectItem);
         // console.log('form value');
        //console.log(that.addProjectForm.value);
        });
//console.log("this.project after");
//console.log(this.project);
 
  }

  public getProjectsList(param:any): void {
    //this.projectItem.Priority="0";
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

   deleteProject(id:any): void {
     this.projectsService.deleteProject(id)
         .subscribe(projects => {
          console.log('Project deleted'); 
          this.getProjectsList(false);
         });
   }
}
