import { TestBed, async, inject } from '@angular/core/testing';
import {
  HttpModule,
  Http,
  Response,
  ResponseOptions,
  XHRBackend
} from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { UsersService } from './users.service';
import { Users } from '../shared/models/users.model';

describe('UsersService', () => {
  let userService: UsersService;
  //let httpMock: HttpTestingController;
  
  beforeEach(() => TestBed.configureTestingModule({
    imports:[HttpModule],
    providers:[UsersService,
        { provide: XHRBackend, useClass: MockBackend }
      ]

  }));
  // userService = TestBed.get(UsersService);
  // httpMock = TestBed.get(HttpTestingController);

  it('should be created', () => {
    const service: UsersService = TestBed.get(UsersService);
    expect(service).toBeTruthy();
  });

    describe('getUsersList()', () => {
            
      it('should return an List of Users',
      async(inject([UsersService, XHRBackend], (userService, mockBackend) => {
        const service: UsersService = TestBed.get(UsersService);
        service.url="www.example.com/"
        const mockResponse = [
            { User_ID: 0, FirstName: 'FirstName 0', LastName: 'LastName 0' },
            { User_ID: 1, FirstName: 'FirstName 1', LastName: 'LastName 1' },
            { User_ID: 2, FirstName: 'FirstName 2', LastName: 'LastName 2' },
            { User_ID: 3, FirstName: 'FirstName 3', LastName: 'LastName 3' },
        ];

        mockBackend.connections.subscribe((connection) => { 

          connection.mockRespond(new Response(new ResponseOptions({
              body: JSON.stringify(mockResponse)
          })));
        });

        userService.getUsersList().subscribe((users:Users[]) => {
          console.log('users');
          console.log(users);
          expect(users.length).toBe(4);
          expect(users[0].FirstName).toEqual('FirstName 0');
          expect(users[1].FirstName).toEqual('FirstName 1');
          expect(users[2].FirstName).toEqual('FirstName 2');
          expect(users[3].FirstName).toEqual('FirstName 3');
        });
      
      })));


      it('should return an selected User details',
      async(inject([UsersService, XHRBackend], (userService, mockBackend) => {
        const service: UsersService = TestBed.get(UsersService);
        service.url="www.example.com/1"
        const mockResponse = [
            { User_ID: 0, FirstName: 'FirstName 0', LastName: 'LastName 0' },
            { User_ID: 1, FirstName: 'FirstName 1', LastName: 'LastName 1' },
            { User_ID: 2, FirstName: 'FirstName 2', LastName: 'LastName 2' },
            { User_ID: 3, FirstName: 'FirstName 3', LastName: 'LastName 3' },
        ];

        mockBackend.connections.subscribe((connection) => { 

          connection.mockRespond(new Response(new ResponseOptions({
              body: JSON.stringify(mockResponse[1])
          })));
        });

        userService.getUsersList().subscribe((users:Users) => {
          console.log('users');
          console.log(users);
          expect(users.User_ID).toEqual(1);
          expect(users.FirstName).toEqual('FirstName 1');
        });
      
      })));

    });


});
