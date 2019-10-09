import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { CommonModule } from "@angular/common";
import {Router} from '@angular/router';
import { FilterPipe } from '../shared/filter.pipe';
import {Users} from "../shared/models/users.model";
import { UsersService } from '../services/users.service';
//import { AddUserComponent } from '../add-user/add-user.component';
import { Observable } from 'rxjs'
import { OrderPipe } from 'ngx-order-pipe';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css'],
  providers: [UsersService,OrderPipe] 
})
export class ViewUsersComponent implements OnInit {

  //users:Users[];
  userItem:Users=new Users();
  //@Input() addUserComponent: AddUserComponent;
  @Input() users : Users[];
  @Output() onGetUserByID = new EventEmitter<boolean>(); 
  @Output() onDeleteUser = new EventEmitter<boolean>(); 
  sortstring :string ="FirstName";
  reverse: boolean = true;
  sortedCollection: any[];
  searchText : string;
  constructor(private usersService: UsersService,private orderPipe: OrderPipe) { 

    this.sortedCollection = orderPipe.transform(this.users, "FirstName");
    console.log(this.sortedCollection);
  }  

  ngOnInit() {
    console.log('view user init');
    //this.getUsersList(); 
  }

  getUsersList(): void {
    this.usersService.getUsersList()
        .subscribe(users => {
          console.log('users=')
          console.log(users);
          this.users = users;
          //console.log('this.users=' + this.users);
                    console.log('this.users.length=' + this.users.length);
                    console.log('this.users[0].FirstName=' + this.users[0].FirstName);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }
  //@HostListener('click')
   getUserByID(id:any): void {
    //this.addUserComponent.loadData(id);
    this.onGetUserByID.emit(id);
    
      
   }

   editUser(id:any,item:Users): void {
    this.usersService.getUserByID(id)
        .subscribe(users => this.userItem = users);
   }

   deleteUser(id:any,user:Users):void{
    if(confirm("Are you sure to delete'"+user.FirstName+" "+user.LastName+"'")) {
      this.onDeleteUser.emit(id);
    }
   }

   sortresult(sort:string){

    this.sortstring=sort;
    console.log("search criteria "+this.sortstring);

    
   }
   
  

}
