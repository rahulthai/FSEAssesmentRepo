import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import {Http,Headers,Response,RequestOptions} from "@angular/http";
import {Projects} from "../shared/models/projects.model";
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  
  public url:string="http://localhost:8070/api/project/";


  constructor(private _http: Http) { }

  private handleError(error: any) { 
    let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    return Observable.throw(error);
  }
  getProjectsList(){
    console.log(this.url+"getallprojects");
       return this._http.get(this.url+"getallprojects")
       .pipe(map((response:Response) => response.json()));
    }

    addProject(item:Projects){
 
      //let body = JSON.stringify(item);
       
      let headers = new Headers({ 'Content-Type': 'application/json'
      ,'Access-Control-Allow-Origin':'*' ,
      "Access-Control-Allow-Methods": "DELETE, POST, GET, OPTIONS",
      "Access-Control-Allow-Headers": "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
    });
    
      let options = new RequestOptions({ headers: headers });
       
       return this._http.post(this.url+"InsertProjectAsync",item, options)
       
      .pipe(map((response:Response)=>response.json()));
       
       }
       
      getProjectByID(id:any){
       console.log("service id "+id);
      return this._http.get(this.url+"GetProjectAsync/"+id)
       
       .pipe(map((response:Response)=>response.json()));
       
       }
       
      editProject(item:number){
       
      let body = JSON.stringify(item);
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this._http.put(this.url+"UpdateProject/"+item, body, options)
      .pipe(map((response:Response)=>response.json()));
       
       }  

       deleteProject(id:any){
       
        return this._http.delete(this.url+"DeleteProject/"+id)
         
         .pipe(map((response:Response)=>response.json()));
         
         }



}
