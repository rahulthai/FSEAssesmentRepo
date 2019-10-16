import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserComponent } from './add-user.component';
import { NgModule } from '@angular/core';
import {Users} from "../shared/models/users.model";
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { UsersService } from '../services/users.service';

describe('AddUserComponent', () => {
  let component: AddUserComponent;
  let fixture: ComponentFixture<AddUserComponent>;
  let userObjTest = new Users();

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers:[UsersService],
      declarations: [ AddUserComponent ],
      imports:[FormsModule,HttpModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('reset model',()=>{
    component.resetModel();
    var modelObj = component.user;
    expect(modelObj).toEqual(userObjTest);
  })
});
