import styled from 'styled-components';
import { NavLink as Link } from 'react-router-dom'
export const PrimaryNav = styled.nav`
  z-index: 14;
  height: 40px;
  display: flex;
  --bs-bg-opacity: 1;
  background-color: rgba(var(--bs-info-rgb),var(--bs-bg-opacity))!important;
  justify-content: space-between;
`
export const MenuLink = styled(Link)`
  color: #fff;
  display: flex;
  cursor: pointer;
  align-items: center;
  text-decoration: none;
  padding: 0 1.2rem;
  height: 100%;
  &.active {
    color: #151d61;
  }
`

export const Menu = styled.div`
  display: flex;
  align-items: center;
  margin-right: -25px;
  @media screen and (max-width: 768px) {
    display: none;
  }
`