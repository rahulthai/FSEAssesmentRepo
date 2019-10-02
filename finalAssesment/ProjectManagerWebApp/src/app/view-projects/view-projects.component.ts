import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { CommonModule } from "@angular/common";
import {Router} from '@angular/router';
import { FilterPipe } from '../shared/filter.pipe';
import {Projects} from "../shared/models/projects.model";
import { ProjectsService } from '../services/projects.service';
import { Observable } from 'rxjs'
import { OrderPipe } from 'ngx-order-pipe';

@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css'],
  providers: [ProjectsService,OrderPipe] 
})
export class ViewProjectsComponent implements OnInit {

  projectItem:Projects=new Projects();
  //@Input() addUserComponent: AddUserComponent;
  @Input() projects : Projects[];
  @Output() onGetProjectByID = new EventEmitter<boolean>(); 
  @Output() onDeleteProject = new EventEmitter<boolean>(); 
  sortstring :string ="Project";
  reverse: boolean = true;
  sortedCollection: any[];
  constructor(private projectsService: ProjectsService,private orderPipe: OrderPipe) { 

    this.sortedCollection = orderPipe.transform(this.projects, "Project");
    console.log(this.sortedCollection);
  }  

  ngOnInit() {
    console.log('view project init');
  }
  getProjectByID(id:any): void {
    this.onGetProjectByID.emit(id);
   }

   editProject(id:any,item:Projects): void {
    this.projectsService.getProjectByID(id)
        .subscribe(projects => this.projectItem = projects);
   }

   deleteProject(id:any,project:Projects):void{
    if(confirm("Are you sure to delete Project - '"+project.Project)) {
      this.onDeleteProject.emit(id);
    }
   }

   sortresult(sort:string){

    this.sortstring=sort;
    console.log("search criteria "+this.sortstring);
   }

}
