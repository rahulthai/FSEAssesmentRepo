import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {Router, ActivatedRoute} from '@angular/router';
import { TasksComponent } from './tasks.component';
import { FilterPipe } from '../shared/filter.pipe';
import { AddTaskComponent } from '../add-task/add-task.component';
import { ViewTasksComponent } from '../view-tasks/view-tasks.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { OrderModule } from 'ngx-order-pipe';

describe('TasksComponent', () => {
  let component: TasksComponent;
  let fixture: ComponentFixture<TasksComponent>;

  const fakeActivatedRoute = {
    snapshot: { data: {  } }
  } as ActivatedRoute;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FilterPipe, TasksComponent,AddTaskComponent,ViewTasksComponent ]
      ,imports:[FormsModule,HttpModule,OrderModule],
      providers:[{provide: ActivatedRoute, useValue: fakeActivatedRoute}]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
