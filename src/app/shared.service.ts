import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {


  private readonly ApiUrl = "https://localhost:5001/api";
  private readonly PhotoUrl = "https://localhost:5001//Photos";



  constructor( private http:HttpClient) { }


  getDepList():Observable<any[]>{
    return this.http.get<any>(this.ApiUrl + "/Department");
  }


  addDepartment(val:any){
    return this.http.post(this.ApiUrl + "/Department",val);
  }

  updateDepartment(val:any){
    return this.http.put(this.ApiUrl + "/Department",val);
  }


  deleteDepartment(val:any){
    return this.http.delete(this.ApiUrl+'/Department/'+val);
  }

  //CRUD For Employee
  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.ApiUrl + "/Employee");
  }

  addEmployee(val:any){
    return this.http.post(this.ApiUrl + "/Employee",val);
  }

  updateEmployee(val:any){
    return this.http.put(this.ApiUrl + "/Employee",val);
  }

  deleteEmployee(val:any){
    return this.http.delete(this.ApiUrl+'/employee/'+val);
  }
  uploadPhoto(val:any){
    return this.http.post(this.ApiUrl + "/Employee/SaveFile",val);
    }

    getAllDepartmentName():Observable<any[]>{
      return this.http.get<any[]>(this  .ApiUrl + "/Employeee/GetAllDepartmentNames")
    }

}
