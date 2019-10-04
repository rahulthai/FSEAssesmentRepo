import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParenttaskmodalComponent } from './parenttaskmodal.component';

describe('ParenttaskmodalComponent', () => {
  let component: ParenttaskmodalComponent;
  let fixture: ComponentFixture<ParenttaskmodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParenttaskmodalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParenttaskmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
