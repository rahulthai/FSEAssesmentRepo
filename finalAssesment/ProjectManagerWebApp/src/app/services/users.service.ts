import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import {Http,Headers,Response,RequestOptions} from "@angular/http";
import {Users} from "../shared/models/users.model";
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

   public url:string="http://localhost:8070/api/users/";
  //   id:number;
  //  private userid = new BehaviorSubject(id);
  //  currentMessage = this.userid.asObservable();

  constructor(private _http: Http) { }

  private handleError(error: any) { 
    let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    return Observable.throw(error);
  }
  getUsersList(){
    console.log(this.url+"getallusers");
       return this._http.get(this.url+"getallusers")
       .pipe(map((response:Response) => response.json()));
    }

    addUser(item:Users){
 
      //let body = JSON.stringify(item);
       
      let headers = new Headers({ 'Content-Type': 'application/json'
      ,'Access-Control-Allow-Origin':'*' ,
      "Access-Control-Allow-Methods": "DELETE, POST, GET, OPTIONS",
      "Access-Control-Allow-Headers": "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
    });
    
      let options = new RequestOptions({ headers: headers });
       
       return this._http.post(this.url+"InsertUserAsync",item, options)
       
      .pipe(map((response:Response)=>response.json()));
       
       }

       updateUser(item:Users){
 
        //let body = JSON.stringify(item);
         
        let headers = new Headers({ 'Content-Type': 'application/json'
        ,'Access-Control-Allow-Origin':'*' ,
        "Access-Control-Allow-Methods": "DELETE, POST, PUT, GET, OPTIONS",
        "Access-Control-Allow-Headers": "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
      });
      
        let options = new RequestOptions({ headers: headers });
         
         return this._http.put(this.url+"UpdateUser",item, options)
         
        .pipe(map((response:Response)=>response.json()));
         
         }
       
      getUserByID(id:any){
       console.log("service id "+id);
      return this._http.get(this.url+"GetUserAsync/"+id)
       
       .pipe(map((response:Response)=>response.json()));
       
       }
       
      editUser(item:number){
       
      let body = JSON.stringify(item);
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this._http.put(this.url+"UpdateUser/"+item, body, options)
      .pipe(map((response:Response)=>response.json()));
       
       }  

       deleteUser(id:any){
       
        return this._http.delete(this.url+"DeleteUser/"+id)
         
         .pipe(map((response:Response)=>response.json()));
         
         }

}
