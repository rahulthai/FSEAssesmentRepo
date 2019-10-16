import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectmodalComponent } from './projectmodal.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AddProjectComponent } from 'src/app/add-project/add-project.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { OrderModule } from 'ngx-order-pipe';
import { ProjectsService } from 'src/app/services/projects.service';
import { FilterPipe } from '../../shared/filter.pipe';

describe('ProjectmodalComponent', () => {
  let component: ProjectmodalComponent;
  let fixture: ComponentFixture<ProjectmodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FilterPipe, ProjectmodalComponent,AddProjectComponent ],
      imports:[FormsModule,CommonModule,OrderModule,HttpModule,
        ReactiveFormsModule],
      providers: [ProjectsService, NgbActiveModal]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
