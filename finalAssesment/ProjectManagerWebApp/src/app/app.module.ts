import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component, Injectable } from '@angular/core';
//import { CommonModule, DatePipe } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms'; 
import { AppComponent } from './app.component';

import { MenuComponent } from './menu/menu.component';
import { ProjectsComponent } from './projects/projects.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UsermodalComponent } from './modal/usermodal/usermodal.component';
//import { ParenttaskmodalComponent } from './modal/parenttaskmodal/parenttaskmodal.component';
// import { AddTaskComponent } from './add-task/add-task.component';
// import { ViewTasksComponent } from './view-tasks/view-tasks.component';


//import { AddUserComponent } from './add-user/add-user.component';
//import { ViewUsersComponent } from './view-users/view-users.component';
// import { AddProjectComponent } from './add-project/add-project.component';
// import { ViewProjectsComponent } from './view-projects/view-projects.component';

@NgModule({
  declarations: [
    AppComponent,
    
    MenuComponent
    
  
    
    //ParenttaskmodalComponent,
    
    // AddTaskComponent,
    
    // ViewTasksComponent
       
    //,AddUserComponent,
    
    //ViewUsersComponent
    // ,
    // AddProjectComponent,
    // ViewProjectsComponent
  ],
  imports: [
    BrowserModule,
    //CommonModule,
    FormsModule,
    AppRoutingModule,
    HttpModule,NgbModule
  ],
  //exports:[FilterPipe],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
