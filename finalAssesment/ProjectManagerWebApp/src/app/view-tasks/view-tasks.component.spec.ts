import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewTasksComponent } from './view-tasks.component';
import { FilterPipe } from '../shared/filter.pipe';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { OrderModule } from 'ngx-order-pipe';
import { ActivatedRoute } from '@angular/router';

describe('ViewTasksComponent', () => {
  let component: ViewTasksComponent;
  let fixture: ComponentFixture<ViewTasksComponent>;
  const fakeActivatedRoute = {
    snapshot: { data: {  } }
  } as ActivatedRoute;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FilterPipe, ViewTasksComponent ]
      ,imports:[FormsModule, HttpModule, OrderModule],
      providers:[{provide: ActivatedRoute, useValue: fakeActivatedRoute}]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
