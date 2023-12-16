import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradesTableComponent } from './grades-table.component';

describe('GradesTableComponent', () => {
  let component: GradesTableComponent;
  let fixture: ComponentFixture<GradesTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GradesTableComponent]
    });
    fixture = TestBed.createComponent(GradesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
