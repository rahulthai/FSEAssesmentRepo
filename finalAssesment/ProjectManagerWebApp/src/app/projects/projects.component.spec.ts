import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsComponent } from './projects.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AddProjectComponent } from '../add-project/add-project.component';
import { ViewProjectsComponent } from '../view-projects/view-projects.component';
import { OrderModule } from 'ngx-order-pipe';
import { FilterPipe } from '../shared/filter.pipe';

describe('ProjectsComponent', () => {
  let component: ProjectsComponent;
  let fixture: ComponentFixture<ProjectsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FilterPipe, ProjectsComponent,AddProjectComponent,ViewProjectsComponent ]
      ,imports:[FormsModule,HttpModule,OrderModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
