import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl = "http://localhost:5150/api";
readonly PhotoUrl = "http://localhost:5150/Photos"

  constructor(private http:HttpClient) { }

  getDepList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDepartment(val:any): Observable<any[]>{
    return this.http.post<any>(this.APIUrl + '/department',val);
  }

  updateDepartment(val:any): Observable<any[]>{
    return this.http.put<any>(this.APIUrl + '/department',val);
  }
  deleteDepartment(val:any): Observable<any[]>{
      return this.http.delete<any>(this.APIUrl + '/department'+val);
  }

  getEmpList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/employee');
  }

  addEmployee(val:any): Observable<any[]>{
    return this.http.post<any>(this.APIUrl + '/employee',val);
  }

  updateEmployee(val:any): Observable<any[]>{
    return this.http.put<any>(this.APIUrl + '/employee',val);
  }
  deleteEmployee(val:any): Observable<any[]>{
      return this.http.delete<any>(this.APIUrl + '/employee'+val);
  }

  UploadPhoto(val:any){
    return this.http.post(this.APIUrl+'/Employee/SaveFile',val);
  }

  getAllDepartmentNames(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/Employee/GetAllDepartmentNames');
  }
  
}
