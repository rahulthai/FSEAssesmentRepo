import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
  declarations:[ ProjectsComponent,
    TasksComponent,
    UsersComponent
    ,AddProjectComponent,
    ViewProjectsComponent
  ]

})
export class AppRoutingModule { }
