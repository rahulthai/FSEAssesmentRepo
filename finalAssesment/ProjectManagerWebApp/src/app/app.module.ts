import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { ProjectsComponent } from './projects/projects.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
// import { AddProjectComponent } from './add-project/add-project.component';
// import { ViewProjectsComponent } from './view-projects/view-projects.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent
    // ,
    // AddProjectComponent,
    // ViewProjectsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
