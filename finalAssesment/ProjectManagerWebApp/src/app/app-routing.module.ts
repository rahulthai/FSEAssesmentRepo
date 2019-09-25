import { NgModule } from '@angular/core';
import { FormsModule  } from "@angular/forms"; 
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from "@angular/common";
import { FilterPipe } from './shared/filter.pipe';
import { ProjectsComponent } from './projects/projects.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
import { AddProjectComponent } from './add-project/add-project.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';

//const routes: Routes = [];
const appRoutes: Routes = [
  { path: 'Projects', component: ProjectsComponent },
  { path: 'Tasks', component: TasksComponent },
  { path: 'Users', component: UsersComponent }
];



@NgModule({
  imports: [RouterModule.forRoot(appRoutes),FormsModule,CommonModule],
  exports: [RouterModule],
  declarations:[FilterPipe, ProjectsComponent,
    TasksComponent,
    UsersComponent
    ,AddProjectComponent,
    ViewProjectsComponent
  ]

})
export class AppRoutingModule { }
