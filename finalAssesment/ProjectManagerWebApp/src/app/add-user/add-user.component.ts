import { Component,Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup,FormControl ,ReactiveFormsModule, NgForm } from '@angular/forms';
//import { CommonModule } from "@angular/common";
import { FilterPipe } from '../shared/filter.pipe';
import {Users} from "../shared/models/users.model";
import { UsersService } from '../services/users.service';
//import { ViewUsersComponent } from '../view-users/view-users.component';
import { Observable } from 'rxjs'
//import { format } from 'path';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css'],
  providers: [UsersService] 
})
export class AddUserComponent implements OnInit {

  //user:Users= new Users();;
  addUserForm: FormGroup;
  @Input() user : Users=new Users();
  @Output() onAddedUser = new EventEmitter<boolean>();
  constructor(private usersService: UsersService) { }

  ngOnInit() {
    //this.user ={FirstName="",LastName="",Employee_ID=""};
    //  this.user.FirstName="";
    //  this.user.LastName="";
    //  this.user.Employee_ID="";

    this.addUserForm =this.createUserFormGroup()
    this.addUserForm.patchValue(this.user);
  }

  createUserFormGroup() {
    return new FormGroup({
        FirstName: new FormControl(),
        LastName: new FormControl(),
        Employee_ID: new FormControl()
       })
  }

  loadData(id:number){
    var that = this;
    
    console.log("this.user in add user");
     this.usersService.getUserByID(id)
         .subscribe(u =>  {
          console.log("this.user inside");
            
          // that.addUserForm=this.createUserFormGroup();
          // this.addUserForm.patchValue(u);
          that.user.User_ID=u.User_ID;
            that.user.FirstName=u.FirstName;
            that.user.LastName=u.LastName;
            that.user.Employee_ID=u.Employee_ID;
          //this.ngOnInit();
          console.log(that.user);
          console.log('form value');
        //console.log(that.addUserForm.value);
        });
console.log("this.user after");
console.log(this.user);
 
  }

  onSubmit(form:NgForm){
    
    if(form.valid){
    console.log('form validated');
    console.log(form.value);
    console.log(this.user);
    if(this.user.User_ID==undefined || this.user.User_ID<=0){   
      this.addUser(this.user);
    }
    else{
      this.updateUser(this.user);
    }
   //this.viewUsersComponent.getUsersList();
  
    }
    else
    {
      console.log('form not valid');
      return;
    }
  }

  resetModel(){

    this.user = new Users();
  }

  addUser(item:Users): void {
    this.usersService.addUser(item)
        .subscribe(users => {this.user = users;
          console.log('user added successfully')
          this.onAddedUser.emit();
        });
  }



  updateUser(item:Users): void {
    this.usersService.updateUser(item)
        .subscribe(users => {this.user = users;
          console.log('user updated successfully')
          this.onAddedUser.emit();
        });
  }

}
