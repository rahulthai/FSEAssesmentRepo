import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import {Http,Headers,Response,RequestOptions} from "@angular/http";
import {Tasks} from "../shared/models/tasks.model";
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  public url:string="http://localhost:8070/api/tasks/";
  //public url:string="http://localhost:52178/api/tasks/";


  constructor(private _http: Http) { }

  private handleError(error: any) { 
    let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    return Observable.throw(error);
  }
  getTasksList(){
    console.log(this.url+"getalltasks");
       return this._http.get(this.url+"getalltasks")
       .pipe(map((response:Response) => response.json()));
    }

    getTasksListByProject(projectId:any){
      console.log(this.url+"getTasksListByProjectId");
         return this._http.get(this.url+"getTasksListByProjectId/"+projectId)
         .pipe(map((response:Response) => response.json()));
      }

    GetParentTasksList(){
      console.log(this.url+"GetParentTasks");
         return this._http.get(this.url+"GetParentTasks")
         .pipe(map((response:Response) => response.json()));
      }

    addTask(item:Tasks){
 
      //let body = JSON.stringify(item);
       
      let headers = new Headers({ 'Content-Type': 'application/json'
      ,'Access-Control-Allow-Origin':'*' ,
      "Access-Control-Allow-Methods": "DELETE, POST, GET, OPTIONS",
      "Access-Control-Allow-Headers": "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
    });
    
      let options = new RequestOptions({ headers: headers });
       
       return this._http.post(this.url+"InsertTaskAsync",item, options)
       
      .pipe(map((response:Response)=>response.json()));
       
       }

       updateTask(item:Tasks){
 
        //let body = JSON.stringify(item);
         
        let headers = new Headers({ 'Content-Type': 'application/json'
        ,'Access-Control-Allow-Origin':'*' ,
        "Access-Control-Allow-Methods": "DELETE, POST,PUT, GET, OPTIONS",
        "Access-Control-Allow-Headers": "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
      });
      
        let options = new RequestOptions({ headers: headers });
         
         return this._http.put(this.url+"UpdateTask",item, options)
         
        .pipe(map((response:Response)=>response.json()));
         
         }
       
      getTaskByID(id:any){
       console.log("service id "+id);
      return this._http.get(this.url+"GetTaskByID/"+id)
       
       .pipe(map((response:Response)=>response.json()));
       
       }
       
      editTask(item:number){
       
      let body = JSON.stringify(item);
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this._http.put(this.url+"UpdateTask/"+item, body, options)
      .pipe(map((response:Response)=>response.json()));
       
       }  

       deleteTask(id:any){
       
        return this._http.delete(this.url+"DeleteTask/"+id)
         
         .pipe(map((response:Response)=>response.json()));
         
         }
}
