import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAddessComponent } from './add-addess.component';

describe('AddAddessComponent', () => {
  let component: AddAddessComponent;
  let fixture: ComponentFixture<AddAddessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAddessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAddessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
