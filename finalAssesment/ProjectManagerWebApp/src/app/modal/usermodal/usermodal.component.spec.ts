import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsermodalComponent } from './usermodal.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderModule } from 'ngx-order-pipe';
import { HttpModule } from '@angular/http';
import { UsersService } from 'src/app/services/users.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AddUserComponent } from 'src/app/add-user/add-user.component';

describe('UsermodalComponent', () => {
  let component: UsermodalComponent;
  let fixture: ComponentFixture<UsermodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsermodalComponent, AddUserComponent ],
      imports:[FormsModule,CommonModule,OrderModule,HttpModule,
        ReactiveFormsModule],
      providers: [UsersService, NgbActiveModal]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsermodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
