import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { lastValueFrom, throwError } from 'rxjs';


@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
 
  //inject the http client 
  //implemts onInit toinitialize the component 
  private http = inject(HttpClient)
  protected title = 'Dating app';
  protected members = signal<any>([]); // i use signal bexause when i install angular i install  provideZonelessChangeDetection(),
  //help me that any change do will reflect to the html 

  //  async ngOnInit() {
    // return an observable of the response body
   //observable for managing async data streams, we have to subscribe 
   //next give me the error, error: when i have an error, when is completed it return a message 
  //  this.http.get('https://localhost:5001/api/members').subscribe ({
  //   next: response => this.members.set(response),
  //   error: error=> console.log(error),
  //   complete: () => console.log('complete the http request')

  //  })
  // when i use async and return a promise
  
  // }

  async ngOnInit(){
  this.members.set(await this.GetMembers())
  }



 async  GetMembers(){
  //instead of returning an observable it return a promise

  try{
     return  lastValueFrom(this.http.get('https://localhost:5001/api/members'));
  }
   catch(error)
   {
     console.error(error);
    throw error
    
   }
  }
 
}
