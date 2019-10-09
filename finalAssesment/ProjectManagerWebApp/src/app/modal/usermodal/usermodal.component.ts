import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {Users} from "../../shared/models/users.model";
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-usermodal',
  templateUrl: './usermodal.component.html',
  styleUrls: ['./usermodal.component.css'],
  providers: [UsersService] 
})
export class UsermodalComponent implements OnInit {
  @Input() public user:any;
  @Output() passEntry: EventEmitter<any> = new EventEmitter();
  userItem:Users= new Users();
  usersList:Users[];
  sortstring:string="FirstName";
  selectedValue:string;
  error:any={isError:false,errorMessage:''};

  constructor( public activeModal: NgbActiveModal,private usersService: UsersService) {
    
    
   }

  ngOnInit() {
    console.log(this.user);
    this.selectedValue = this.user && this.user.userid==0?"":this.user.userid; 
    this.sortstring ="FirstName";
    this.getUsersList();
  }

  onChange(event) {
   this.error={isError:false,errorMessage:''};
   // let selectElementText = event.target['options']
   //[event.target['options'].selectedIndex].text;
    //console.log(selectElementText);
    //console.log(this.selectedValue);
    let name = event.target.options[event.target.options.selectedIndex].text;

    this.user.Fullname = name;
    this.user.userid = this.selectedValue;
    console.log(this.user);
}
deleteAlert(){

  this.error.isError=false;
  this.error.errorMessage="";
}
  passBack() {
    //console.log(FirstName);
    //this.user.Fullname =
     //console.log(document.querySelector("user"));
     //console.log('seelctor');
    //this.user.userid = this.selectedValue;
    if(this.user.userid>0 && this.user.Fullname!=""){
      this.passEntry.emit(this.user);
      this.activeModal.close(this.user);
    }
    else{
    this.error.isError=true;
    this.error.errorMessage="Please select a user";
    }
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
}
