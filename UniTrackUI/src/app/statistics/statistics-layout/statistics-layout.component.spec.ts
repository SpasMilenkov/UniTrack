import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticsLayoutComponent } from './statistics-layout.component';

describe('StatisticsLayoutComponent', () => {
  let component: StatisticsLayoutComponent;
  let fixture: ComponentFixture<StatisticsLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StatisticsLayoutComponent]
    });
    fixture = TestBed.createComponent(StatisticsLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
