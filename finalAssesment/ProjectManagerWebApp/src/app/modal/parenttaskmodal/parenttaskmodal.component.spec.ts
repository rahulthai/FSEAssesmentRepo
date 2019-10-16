import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ParenttaskmodalComponent } from './parenttaskmodal.component';
import { NgModule } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TasksService } from 'src/app/services/tasks.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderModule } from 'ngx-order-pipe';
import {Http,Headers,Response,RequestOptions, HttpModule} from "@angular/http";
import { AddTaskComponent } from 'src/app/add-task/add-task.component';

describe('ParenttaskmodalComponent', () => {
  let component: ParenttaskmodalComponent;
  let fixture: ComponentFixture<ParenttaskmodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParenttaskmodalComponent, AddTaskComponent],
      imports:[FormsModule,CommonModule,OrderModule,HttpModule,
        ReactiveFormsModule],
      providers: [TasksService, NgbActiveModal] 
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParenttaskmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
