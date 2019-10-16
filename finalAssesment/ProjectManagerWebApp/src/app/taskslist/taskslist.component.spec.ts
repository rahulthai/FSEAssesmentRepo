import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskslistComponent } from './taskslist.component';
import { ActivatedRoute } from '@angular/router';
import { FilterPipe } from '../shared/filter.pipe';
import { AddTaskComponent } from '../add-task/add-task.component';
import { ViewTasksComponent } from '../view-tasks/view-tasks.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { OrderModule } from 'ngx-order-pipe';

describe('TaskslistComponent', () => {
  let component: TaskslistComponent;
  let fixture: ComponentFixture<TaskslistComponent>;
  
  const fakeActivatedRoute = {
    snapshot: { data: {  } }
  } as ActivatedRoute;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FilterPipe, TaskslistComponent,AddTaskComponent,ViewTasksComponent ]
      ,imports:[FormsModule,HttpModule,OrderModule],
      providers:[{provide: ActivatedRoute, useValue: fakeActivatedRoute}]

    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskslistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
