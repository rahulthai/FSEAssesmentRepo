import { Component, OnInit } from '@angular/core';
import {Users} from "../shared/models/users.model";
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [UsersService] 
})
export class UsersComponent implements OnInit {

  userid;
  userItem:Users= new Users();
  usersList:Users[];

  constructor(private usersService: UsersService) { }

  ngOnInit() {
    this.getUsersList();
  }

  getUserByID(event){
    console.log('this is parent '+event)
    this.userid = event;
    this.loadUserData(this.userid);
  }

  loadUserData(id:number){
    var that = this;
    
     this.usersService.getUserByID(id)
         .subscribe(u =>  {that.userItem =u;
          console.log("this.user inside");
            
          // that.user.User_ID=u.User_ID;
          //   that.user.FirstName=u.FirstName;
          //   that.user.LastName=u.LastName;
          //   that.user.Employee_ID=u.Employee_ID;
          //this.ngOnInit();
          console.log(that.userItem);
         // console.log('form value');
        //console.log(that.addUserForm.value);
        });
//console.log("this.user after");
//console.log(this.user);
 
  }

  getUsersList(): void {
    this.usersService.getUsersList()
        .subscribe(users => {
          console.log('users=')
          console.log(users);
          this.usersList = users;
          console.log('this.users=' + this.usersList);
                    console.log('this.users.length=' + this.usersList.length);
                    console.log('this.users[0].FirstName=' + this.usersList[0].FirstName);
          }
          , //Bind to view
          err => {
            // Log errors if any
            console.log('test error '+err);
        });     
  }

   deleteUser(id:any): void {
     this.usersService.deleteUser(id)
         .subscribe(users => {
          console.log('User deleted'); 
          this.getUsersList();
         });
   }


}
