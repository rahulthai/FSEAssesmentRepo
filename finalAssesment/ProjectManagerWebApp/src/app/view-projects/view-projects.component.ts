import { Component, OnInit } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FilterPipe } from '../shared/filter.pipe';
import {Projects} from "../shared/models/projects.model";
import { ProjectsService } from '../services/projects.service';
import { Observable } from 'rxjs'

@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css'],
  providers: [ProjectsService] 
})
export class ViewProjectsComponent implements OnInit {

  projects:Projects[];
  
  constructor(private projectsService: ProjectsService) { }

  ngOnInit() {
    this.getProjectsList(); 
  }

  getProjectsList(): void {
    this.projectsService.getProjectsList()
        .subscribe(projects => {
          console.log('projects=')
          console.log(projects);
          this.projects = projects;
          console.log('this.projects=' + this.projects);
                    console.log('this.projects.length=' + this.projects.length);
                    console.log('this.projects[0].Project=' + this.projects[0].Project);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }

  editProject(id:any,item:Projects): void {
    this.projectsService.editProject(item)
        .subscribe(projects => this.projects = projects);
  }

  deleteProject(id:any): void {
    this.projectsService.deleteProject(id)
        .subscribe(projects => this.projects = projects);
  }


}
